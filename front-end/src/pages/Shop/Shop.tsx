import {useState, useEffect} from "react";
import "./Shop.css";
import Popup from "components/Popup";

interface Product{
  id: number
  name: string
  description: string
  price: number
  accountId: number
}
interface Account{
  id: number
  username: string
  password: string
  locationName: string
  locationRegion: string
  locationVAT: number
  isProvider: boolean
  isPayingVAT: boolean
}
export default function Shop() {
  const [allProducts, setAllProducts] = useState<Product[]>([]);
  const [allProviders, setAllProviders] = useState<Account[]>([]);

  const [popupIsDisplayed, setPopupIsDisplayed] = useState<boolean>(false);

  useEffect(()=>{
    handleFetches();
  }, []);
  const handleFetches = async ()=>{
    let products : any[] = [];
    let providers : any[] = [];

    await handleProductFetch().then((response)=>products=response);
    for(let i = 0;i<products.length;i++){
      await handleAllProviderFetch(products[i]).then((response)=>providers.push(response));
    }

    await setAllProducts(products);
    await setAllProviders(providers);
  }
  const handleProductFetch = async ()=>{
    let tempAllProducts : Product[] = [];
    
    await fetch("https://localhost:7231/Product",{
      method: "GET"
    })
    .then(async (response)=>{
      if(response.status === 200){
          tempAllProducts = await response.json();
      }
    });
    return tempAllProducts;
  }
  const handleAllProviderFetch = async (product : Product)=>{
    let tempAllProviders : Account[] = [];


        await fetch("https://localhost:7231/Account/"+product.accountId,{
          method: "GET"
        })
        .then(async (response)=>{
          if(response.status === 200){
              tempAllProviders.push(await response.json());
          }
        });
      
    
    
    return tempAllProviders[0];
  }
  const ready = () => {
    if(allProducts.length > 0){
      if(allProviders.length > 0){
        return true;
      }
    }
    return false;
  };
  const handleBuyClick = (productId : number) =>{
    let myCart = sessionStorage.getItem("myCart");
    if(myCart !== null){
      myCart+=";"+productId;
      sessionStorage.setItem("myCart", myCart);
    }
    else{
      sessionStorage.setItem("myCart", productId.toString());
    }
    setPopupIsDisplayed(true);
    setTimeout(()=>{
      setPopupIsDisplayed(false);
    }, 2000)
  }

  let productIndex=-1;
    return (
  <>
  <Popup
    isDisplayed={popupIsDisplayed}
    message={"Product added to cart"}
  />
  <h1>SHOP</h1>
  <div className="initial column-wrapper">
    <div className="shop-item">
      <div className="wrapper">
        <span className="product-name">
          <b>Product's name</b>
        </span>
        <span className="product-description">
          <b>Product's description</b>
        </span>
        <span className="provider-username">
          <b>Provider's username</b>
        </span>
        <span className="provider-vat">
          <b>Is provider paying VAT?</b>
        </span>
        <span className="provider-location-region">
          <b>Country, region</b>
        </span>
        <span className="product-price">
          <b>Price</b>
        </span>
        <span className="inital buy-button"></span>
      </div>
    </div>
  {
    ready()  ?
    allProducts.map((product)=>{
      productIndex++;
      let provider : Account = allProviders[productIndex];
        return(
          <div key={product.id} className="shop-item">
            <div className="wrapper">
              <span className="product-name">{product.name}</span>
              <span className="product-description">{product.description}</span>
              <span className="provider-username">{provider.username}</span>
              <span className="provider-vat">{provider.isPayingVAT === true ? "Yes" : "No"}</span>
              <span className="provider-location-region">{provider.locationName},{provider.locationRegion}</span>
              <span className="product-price">
                <b>{product.price}</b>
              </span>
              <button className="buy-button" onClick={()=>handleBuyClick(product.id)}>
                <span>BUY</span>
              </button>
            </div>
          </div>
        )
      })
      : ""
      }
      
  </div>
    </>
    );
  }