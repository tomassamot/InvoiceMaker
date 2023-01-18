import { useState, useEffect } from "react";

interface Invoice{
  id: number
  dateTime: string
  products: Product[]
  accountId: number
}
interface Product{
  id: number
  name: string
  description: string
  price: number
  priceWithVAT: number
  accountId: number
}
export default function InvoiceList() {
  const [allInvoices, setAllInvoices] = useState<Invoice[]>([]);

  useEffect(()=>{
    const currentAccountId = sessionStorage.getItem("myId");
    fetch("https://localhost:7231/Invoice/GetAll/"+currentAccountId,{
      method: "GET"
    })
    .then((response)=>{
      if(response.status === 200){
        response.json()
        .then((data)=>setAllInvoices(data));
      }
      else{
        response.json()
        .then((error)=>console.error(error));
      }
    })
  }, [])

    return (
  <>
  <h1>INVOICES</h1>
  <div className="column-wrapper">
    {
    allInvoices.length > 0 ?
    allInvoices.map((invoice : Invoice)=>{
      console.warn("invoice.product.length: "+invoice.products.length)
      return(
  <>
    <div className="shop-item">
      <div className="wrapper">
        <span className="product-name">
          <b>Invoice ID</b>
        </span>
        <span className="product-description">
          <b>Date</b>
        </span>
      </div>
    </div>
    <div className="shop-item">
      <div className="wrapper">
        <span className="product-name">
          <b>{invoice.id}</b>
        </span>
        <span className="product-description">
          <b>{invoice.dateTime}</b>
        </span>
      </div>
    </div>
      {invoice.products.length > 0 ?
      invoice.products.map((product : Product)=>{
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
      : ""
    }
      </>
      

)
})
:""

}
</div>
    </>
    );
  }