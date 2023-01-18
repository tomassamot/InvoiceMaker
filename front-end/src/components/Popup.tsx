import {useState, useEffect} from "react";

interface Props{
    isDisplayed : boolean
    message: string
}
export default function Popup(props : Props){
    return(
        <>
        {
            props.isDisplayed ?
            (
                <div className="popup">
                    <span>{props.message}</span>
                </div>
            )
            : ""
        }
        </>
    )
}