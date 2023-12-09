import { useState } from 'react';
import '../css/auth.css'
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import { reset } from '../../../Functions';


const ResetPassword = () =>{
    const handleSubmit = () => {
        try
          {
              reset(emailValue);
          }
          catch(error)
          {
            alert(error);
          }
        };
    const [emailValue, setEmailValue] = useState('');

  function handleEmailChange(event: React.ChangeEvent<HTMLInputElement>) {
    setEmailValue(event.target.value);
  }

 
    return(
        <>
            <div className="auth-container">
                <div className="auth-frame">
                    <div className="mini-logo">
                        <span style={{color: "white"}}>Burger</span>
                        <span style={{color: "yellow", textDecoration: "underline"}}> Bensonhurst</span>
                        <span style={{color: "yellow", fontSize: "2.5vw"}}> EST.1913</span>
                    </div>
                    <input value={emailValue} onChange={handleEmailChange} className="text-box" type="email" placeholder="Email..."></input>
                   <br></br>
                    <Link className="auth-link" to='/signin'>Back</Link>
                   <br></br>
                    <div onClick={handleSubmit} className="auth-button">Send Link</div>
                </div>
            </div>
        </>
    )
}

export default ResetPassword;