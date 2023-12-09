import React, { useEffect, useState  } from 'react';
import "../components/ct.css"
import default_image from "./Images/default_account_logo_png.png"
import { url } from 'inspector';
import { Link } from 'react-router-dom';
import { LogOut, logOut, reloadPage } from './Functions';
import axios from 'axios';

const SiteHeader = () => {
    const [accountImage, setAccountImage] = useState(default_image);
    async function GetUserImage()
    {
        if (localStorage.getItem('BurgerJwtToken') !== null)
        {
            if (localStorage.getItem('Account-image') === null)
            {
                const request = { data: localStorage.getItem('BurgerJwtToken') };
                await axios.post("https://localhost:7048/GetAccountImage", request).then((resp) =>{
                if (`${resp.data}`.length !== 0)
                {
                    localStorage.setItem('Account-image', 'data:image/png;base64, ' + resp.data);
                    reloadPage(1);
                }
                });
            }
            else{
                setAccountImage(localStorage.getItem('Account-image') + '');
            }
        }
    }

    function ShowMenu(){
        let elem = document.getElementById("adopted-links") as HTMLElement;
        elem.style.display = elem.style.display !== "inline-block" ? "inline-block" : "none";

    }

    function ShowAccountLinks(){
        let elem = document.getElementById("account-links") as HTMLElement;
        elem.style.display = elem.style.display !== "inline-block" ? "inline-block" : "none";

    }

    function handleResize() {
        let elem = document.getElementById("adopted-links") as HTMLElement;
        let elem2 = document.getElementById("account-links") as HTMLElement;
        if (window.innerWidth > 1400) {
          elem.style.display = "none";
        } 
        if (window.innerWidth < 1400) {
            elem2.style.display = "none";
        } 
    }

    const AuthorizzationDependent = () => {
        if (localStorage.getItem("BurgerJwtToken") === null){
            return(
                <>
                    <Link to='/signIn' className='adopted-link'>SIGN IN</Link>
                    <Link to='/signUp' className='adopted-link'>SIGN UP</Link>
                </>
            )
        }
        else{
            return(
                <>
                    <Link to='/account' className='adopted-link'>ACCOUNT</Link>
                    <Link onClick={logOut} to='/' className='adopted-link'>LOG OUT</Link>
                </>
            )
        }
    }

    useEffect(() => {
        GetUserImage();
        window.addEventListener("resize", handleResize);
        return () => {
        window.removeEventListener("resize", handleResize);
        };
    }, []);
    return (
        <>
        <div className='header-frame'>
            <Link to="/" id='logo'>
                    <div id='logo-text'>Burger <span style={{color: 'yellow'}}>Bensonhurst</span><br></br></div>
                    <span style={{fontSize: "30px", color: "yellow"}}>EST.1913</span>
            </Link>
            <div id='link-box'>
                <Link to="/menu" className='link-item'>MENU</Link>
                <Link to="/blackBurgers" className='link-item'>
                    BLACK BURGERS
                    <div className='link-item-presc'>NEW</div>
                </Link>  
                <Link to='about' className='link-item'>ABOUT</Link>    
                <Link  to="/bag" className='link-item'>BAG</Link>  
            </div>
            <div onClick={ShowAccountLinks} id="account-box">
                <img style={{height: "100%", width: "100%"}} src={accountImage}></img>
            </div>
            <div className="menu" onClick={ShowMenu}>
                <div className="line"></div>
                <div className="line"></div>
                <div className="line"></div>
            </div>

            <div className='link-box' id="adopted-links">
                <Link to="/menu" className='adopted-link'>MENU</Link>
                <Link to="/blackBurgers" className='adopted-link'>BLACK BURGERS</Link>
                <Link to="/about" className='adopted-link'>ABOUT</Link>
                <Link to="/bag" className='adopted-link'>BAG</Link>
                {AuthorizzationDependent()}
            </div>

            <div onClick={ShowAccountLinks} id='account-links' className='link-box'>
                {AuthorizzationDependent()}
            </div>
            <div style={{margin: "0 20px"}}></div><br></br>
        </div>
        <div className='header-white-line'></div>
        </>
        
    )
}

export default SiteHeader;