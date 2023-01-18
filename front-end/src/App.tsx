import './App.css';
import MainHeader from "components/MainHeader";
import MainFooter from 'components/MainFooter';
import {Routes, Route} from "react-router-dom";
import Home from './pages/Home/Home';
import PageNotFound from './pages/PageNotFound/PageNotFound';
import Login from './pages/Login/Login';
import Register from './pages/Register/Register';
import Shop from './pages/Shop/Shop';
import InvoiceList from './pages/Invoice/InvoiceList';
import Cart from './pages/Cart/Cart';

function App() {
  return (
    <>
      <div className="App">
        <MainHeader/>
        <Routes>
            <Route path='/' element={<Home/>}/>
            <Route path='*' element={<PageNotFound/>}/>
            <Route path='/login' element={<Login/>}/>
            <Route path='/register' element={<Register/>}/>
            <Route path='/shop' element={<Shop/>}/>
            <Route path='/invoices' element={<InvoiceList/>}/>
            <Route path='/cart' element={<Cart/>}/>
        </Routes>
        <MainFooter/>
      </div>
  </>
  );
}

export default App;
