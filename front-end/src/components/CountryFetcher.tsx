import {useState, useEffect} from "react";
import { Country } from "../pages/Register/Register";


export async function fetchCountries(){
    const regions = ["the+caribbean", "south+america", "oceania", "north+america", "middle+east", "europe", "central+america", "asia", "antarctic", "africa"];

    let allCountries : Country[]=[];
    for(let i = 0;i<regions.length;i++){
        /*console.warn("allCountries:")
        for(let j = 0;j<allCountries.length;j++){
            console.warn("country.name: "+allCountries[j].name+", country.region: "+allCountries[j].region+", country.vat: "+allCountries[j].vat)
        }*/
        let regionCountries : Country[] = await fetchCountriesByRegion(regions[i]);
        allCountries.push(...regionCountries);
    }

    return allCountries;
}
async function  fetchCountriesByRegion(region : string){
    let countries : any[] = [];
    await fetch("https://localhost:7231/Country?region="+region,{
      method: "GET"
    })
    .then((response)=>
    response.json())
    .then((data : Country[])=> {countries = data;})
    .catch((error)=>console.error(error));
    return countries;
}