import {useEffect, useState} from "react";
import { useNavigate } from "react-router-dom";
import Popup from "../../components/Popup";

interface Product{
  id: number
  name: string
  description: string
  price: number
  priceWithVAT: number
  accountId: number
}
export default function Cart() {
  const [allProducts, setAllProducts] = useState<Product[]>([])
  
  const [popupIsDisplayed, setPopupIsDisplayed] = useState(false);
  const [popupMessage, setPopupMessage] = useState("");

  const navigate = useNavigate();

  useEffect(()=>{
    handleFetches();
  }, [popupIsDisplayed])
  const handleFetches = async()=>{
    const currentAccountId = sessionStorage.getItem("myId");
    const allProductIds = sessionStorage.getItem("myCart")?.split(";");

    let tempAllProducts : Product[] = [];
    
    if(allProductIds !== undefined && currentAccountId !== null){
      for(let i = 0;i<allProductIds.length;i++){
        const productWithVAT = await fetchProduct(currentAccountId, allProductIds[i]);
        tempAllProducts.push(productWithVAT);
      }
    }
    setAllProducts(tempAllProducts);
  }
  const fetchProduct = async (currentAccountId : string, productId : string)=>{
    let product : Product = {id:0,name:"",description:"",price:0,priceWithVAT:0,accountId:0};
    await fetch("https://localhost:7231/Product/"+currentAccountId+","+productId,{
      method: "GET"
    })
    .then(async (response)=>{
      if(response.status === 200){
        product = await response.json()
      }
    })
    return product;
  }

  const handleInvoiceCreation = ()=>{
    const currentAccountId = sessionStorage.getItem("myId")
    if(currentAccountId === null){
      return;
    }
    fetch("https://localhost:7231/Invoice?accountId="+currentAccountId,{
      method: "POST",
      body: JSON.stringify(allProducts),
	  headers: {
		  "Content-Type": "application/json"
	  }
    })
    .then(async (response)=>{
      if(response.status === 201){
        setPopupMessage("New invoice created!");
        setPopupIsDisplayed(true);
        sessionStorage.removeItem("myCart");
      }
      else{
        setPopupMessage("Unexpected problem occured. Try again.");
        setPopupIsDisplayed(true);
        console.error( await response.text());
      }

      setTimeout(()=>{
        setPopupIsDisplayed(false);
      }, 2000);
    })
  }


  return (
<>
<h1>CART</h1>
<Popup
  isDisplayed={popupIsDisplayed}
  message={popupMessage}
/>
<button style={{backgroundColor: "lightblue", marginBottom: "10px"}} onClick={handleInvoiceCreation}>
  <span>Create invoice</span>
</button>
<div className="column-wrapper">
  <div className="shop-item">
    <div className="wrapper">
      <span className="product-name">
        <b>Product's name</b>
      </span>
      <span className="product-description">
        <b>Product's description</b>
      </span>
      <span className="product-price">
        <b>Price without VAT</b>
      </span>
      <span className="product-price">
        <b>Price with VAT</b>
      </span>
    </div>
  </div>
  {
    allProducts.length > 0 ?
    allProducts.map((product)=>{
      return(
      <div className="shop-item">
        <div className="wrapper">
          <span className="product-name">
            {product.name}
          </span>
          <span className="product-description">
            {product.description}
          </span>
          <span className="product-price">
            {product.price}
          </span>
          <span className="product-price">
            {product.priceWithVAT}
          </span>
        </div>
      </div>
      )
    })
    :""
  }
</div>

  </>
  );
}
