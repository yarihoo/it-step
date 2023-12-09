import React from 'react';
import './App.css';
import SiteHeader from './components/SiteHeader';
import SiteFooter from './components/SiteFooter';
import Home from './components/Pages/Home/Home';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import About from './components/Pages/Home/About';
import brick_wall from "./components/Images/brick_wall.jpg"
import Menu from './components/Pages/Home/Menu';
import BlackBurgers from './components/Pages/Home/BlackBurgers';
import Bag from './components/Pages/Home/Bag';
import SignIn from './components/Pages/Auth/SignIn';
import SignUp from './components/Pages/Auth/SignUp';
import ResetPassword from './components/Pages/Auth/ResetPassword/resetPassword';
import ResetComplete from './components/Pages/Auth/ResetPassword/resetComplete';
import ConfrirmAccount from './components/Pages/Auth/Confirm/ConfirmAccount';
import Account from './components/Pages/Auth/Account/Account';
import ImageCropper from './components/Cropper/ImageCropperModal';

function App() {
  return (
    <div style={{position: 'relative', minHeight: '1500px'}}>
      <Router>
        <SiteHeader/>
        <img className='brick-wall-image' src={brick_wall}></img>
        <Routes>
          <Route path='/' element={<Home/>}/>
          <Route path='/menu' element={<Menu/>}/>
          <Route path='/about' element={<About/>}/>
          <Route path='/blackBurgers' element={<BlackBurgers/>}/>
          <Route path='/bag' element={<Bag/>}/>
          <Route path='/about' element={<About/>}/>
          <Route path='/signIn' element={<SignIn/>}/>
          <Route path='/signUp' element={<SignUp/>}/>
          <Route path='/resetPassword' element={<ResetPassword/>}/>
          <Route path='/resetComplete' element={<ResetComplete/>}/>
          <Route path='/accountConfirm' element={<ConfrirmAccount/>}/>
          <Route path='/account' element={<Account/>}/>
          <Route path='/cropper' element={<ImageCropper/>}/>
        </Routes>
        <SiteFooter/>
      </Router>
    </div>
  );
}

export default App;
