import {NavLink} from "react-router-dom"

export default function MainHeader(){
    return(<>
        <nav>
            <NavLink to="/shop">
                shop
            </NavLink>
            <NavLink to="/invoices">
                invoices
            </NavLink>
            <NavLink to="/cart">
                cart
            </NavLink>
            <NavLink to="/settings">
                settings
            </NavLink>
            <NavLink to="/login">
                login
            </NavLink>
        </nav>
    </>);
}