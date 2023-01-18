import {useState, useEffect} from "react";
import {NavLink, Link} from "react-router-dom"
import {logCurrentUserOut} from "components/Logout";

export default function MainHeader(){
    const [isLoggedIn, setIsLoggedIn]=useState(false);

    useEffect(()=>{
        if(sessionStorage.getItem("myId") !== null){
            setIsLoggedIn(true);
        }
        else{
            setIsLoggedIn(false);
        }
    }, [sessionStorage.getItem("myId")])

    return(
        <nav>
            <NavLink to="/shop" className="nav-link">
                <span>SHOP</span>
            </NavLink>
            <NavLink to="/invoices" className="nav-link">
                <span>INVOICES</span>
            </NavLink>
            <NavLink to="/" className="nav-link">
                <span>HOME</span>
            </NavLink>
            <NavLink to="/cart" className="nav-link">
                <span>CART</span>
            </NavLink>
            {
                isLoggedIn ?
                (
                    <NavLink to="/" className="nav-link" onClick={()=>{sessionStorage.removeItem("myId");window.location.reload();}}>
                        <span>LOGOUT</span>
                    </NavLink>
                )
                :
                (
                    <NavLink to="/login" className="nav-link">
                        <span>LOGIN</span>
                    </NavLink>
                )
            }
        </nav>
        );
}