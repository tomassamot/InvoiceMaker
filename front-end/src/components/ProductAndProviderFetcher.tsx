export default function ProductAndProviderFetcher (){}
/*import {useState, useEffect} from "react";
import { Country } from "../pages/Register/Register";

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
export async function fetchProductsAndProviders(){
    let allProducts = await fetchProducts();
    //let allProviders = await fetchProviders(allProducts);

    return [allProducts, null];
}
async function fetchProducts(){
    let tempAllProducts : Product[] = [];

    await fetch("https://localhost:7231/Product",{
      method: "GET"
    })
    .then((response)=>{
      if(response.status === 200){
        response.json()
        .then((data : Product[])=> {
          tempAllProducts = data;
        })
        .catch((error)=>console.error(error));
      }
    })
    .then(()=>fetchProviders(tempAllProducts))

    return tempAllProducts;
}
async function fetchProviders(allProducts : Product[]){
    let tempAllProviders : Account[] = [];

    for(let i = 0;i<allProducts.length;i++){
        let product = allProducts[i];
        fetch("https://localhost:7231/Account/"+product.accountId,{
          method: "GET"
        })
        .then((response)=>{
          if(response.status === 200){
            response.json()
            .then((data : Account)=>{
              tempAllProviders.push(data);
            })
          }
        });
      }
      return tempAllProviders;
}*/