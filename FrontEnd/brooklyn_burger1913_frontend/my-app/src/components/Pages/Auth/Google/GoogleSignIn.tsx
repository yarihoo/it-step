import React, { useEffect } from 'react'
import { APP_ENV } from '../../../../env';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { reloadPage } from '../../../Functions';

const GoogleSignIn = () => {
    let navigate = useNavigate();
    const handleLogin = async (resp: any) => {
        const {credential} = resp;
        console.log("login google ", credential);
        const data = { token: credential };
        try {
          const response = await axios.post("https://localhost:7048/google/login", data);
          if (response.data.isNewUser === true){
            const response2 = await axios.post("https://localhost:7048/google/registartion", data)
            .then(() =>{
                const response3 = axios.post("https://localhost:7048/google/login", data)
                .then((resp) =>{
                    localStorage.setItem('BurgerJwtToken', resp.data.token);
                    navigate('/');
                }).then(() => {
                    reloadPage(300);
                });
            })
          }
          else{
            localStorage.setItem('BurgerJwtToken', response.data.token);
            navigate('/');
            reloadPage(1);

          }
        } catch (error) {
          alert(error);
        }
    }
    
    useEffect(() => {
        window.google.accounts!.id.initialize({
            client_id: APP_ENV.GOOGLE_CLIENT_ID,    
            callback: handleLogin
        });
        window.google.accounts.id.renderButton(
            document.getElementById('googleButton'),
            {
            size: 'large',
            type: 'icon',   
            })
    },[])
    
    
    return(
        <>
        <h2 style={{color: "white", fontWeight: "lighter"}}>Log In with Google</h2>
        <div className='google-button' id='googleButton'></div>
        </>
    )
}

export default GoogleSignIn;