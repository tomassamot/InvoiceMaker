import {useState, useEffect} from "react";
import { Location } from "pages/Register/Register";

interface Props{
    selectedLocationIndex: number
    allLocations: Location[]
    handleLocationChange: (newLocationIndex : number)=>void
}
export default function LocationDropdown(props : Props){
    setTimeout(()=>{}, 5000);

    const [searchInput, setSearchInput] = useState("");
    const [filteredAllLocations, setFilteredAllLocations] = useState<Location[]>();

    useEffect(()=>{
        setFilteredAllLocations(props.allLocations);
    },[])
    
    const findIndex = (location : Location)=>{
        for(let i = 0;i<props.allLocations.length;i++){
            let currLocation = props.allLocations[i];
            if(currLocation.name === location.name){
                return i;
            }
        }
        return -1;
    }
    const filterLocations = (event : any)=>{
        let currentInput = event.target.value;
        setSearchInput(currentInput);
                
        if(props.allLocations === undefined){
            return;
        }
        if(currentInput === ""){
            setFilteredAllLocations(props.allLocations);
        }

        let newFilteredAllLocations : Location[] = [];

        for(let i = 0;i<props.allLocations.length;i++){
            let location = props.allLocations[i];
            if(location.name.toLocaleLowerCase().includes(currentInput.toLocaleLowerCase())){
                newFilteredAllLocations.push(location);
            }
        }
        setFilteredAllLocations(newFilteredAllLocations);
    }
    return(
        <>
        <label>Location:</label>
        <div className="dropdown">
            <button onClick={(event: { preventDefault: () => void })=> {event.preventDefault();document.getElementById("dropdown-btn")?.classList.toggle("show");}} className="dropbtn">{props.allLocations !== undefined ? props.allLocations[props.selectedLocationIndex].name : ""}</button>
            <div id="dropdown-btn" className="dropdown-content">
                <input type="text" placeholder="Search.." id="search-input" onChange={filterLocations}/>
                {
                    filteredAllLocations !== undefined ?
                    filteredAllLocations.map(location=>{
                    let index = findIndex(location);
                    return(
                        <div key={index} id={index.toString()} className={"dropdown-item"} onClick={()=>props.handleLocationChange(index)}>{location.name}</div>
                    );
                    })
                    : ""
                }
            </div>
        </div>
        </>
    );
}