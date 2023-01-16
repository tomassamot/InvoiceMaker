import './App.css';
import MainHeader from "components/MainHeader";
import MainFooter from 'components/MainFooter';
import {Routes, Route} from "react-router-dom";
import Home from './pages/Home/Home';
import PageNotFound from './pages/PageNotFound/PageNotFound';
import Login from './pages/Login/Login';
import Logout from './pages/Logout/Logout';
import Register from './pages/Register/Register';
import Shop from './pages/Shop/Shop';
import Product from './pages/Shop/Product';
import InvoiceList from './pages/Invoice/InvoiceList';
import Invoice from './pages/Invoice/Invoice';
import Cart from './pages/Cart/Cart';
import Settings from './pages/Settings/Settings';

function App() {
  return (
    <>
      <div className="App">
        <MainHeader/>
        <Routes>
            <Route path='/' element={<Home/>}/>
            <Route path='*' element={<PageNotFound/>}/>
            <Route path='/login' element={<Login/>}/>
            <Route path='/logout' element={<Logout/>}/>
            <Route path='/register' element={<Register/>}/>
            <Route path='/shop' element={<Shop/>}/>
            <Route path='/shop/:id' element={<Product/>}/>
            <Route path='/invoices' element={<InvoiceList/>}/>
            <Route path='/invoices/:id' element={<Invoice/>}/>
            <Route path='/cart' element={<Cart/>}/>
            <Route path='/settings' element={<Settings/>}/>
        </Routes>
        <MainFooter/>
      </div>
  </>
  );
}

export default App;
