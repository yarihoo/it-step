import {useState} from "react";
import "./css/auth.css"
import { Link, useNavigate } from 'react-router-dom';
import GoogleAuth from "./Google/GoogleSignIn";
import GoogleSignIn from "./Google/GoogleSignIn";
import axios from "axios";
import eye from '../../Images/see-password-image.png'

const login = async (Email: string, Password: string) => {
    const data = { email: Email, password: Password };
    try {
      const response = await axios.post("https://localhost:7048/login", data);
      const token = response.data.token;
      localStorage.setItem('BurgerJwtToken', token.result);
      // store the token in localStorage or a cookie
    } catch (error) {
      alert(error);
    }
  };


const SignIn = () =>{
    const [inputType, setInputType] = useState<string>('password');
    const [emailValue, setEmailtValue] = useState<string>('');
    const [passwordValue, setPasswordtValue] = useState<string>('');

    const handleEmailChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setEmailtValue(event.target.value);
      };

    const handlePasswordChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setPasswordtValue(event.target.value);
      };

    const handleClick = () => {
      setInputType(inputType === 'password' ? 'text' : 'password');
    };

    const navigate = useNavigate();
    const handleSubmit = () => {
        try
          {
            login(emailValue, passwordValue).then(()=>{
            if (localStorage.getItem('BurgerJwtToken') !== null){
              navigate("/");
              window.location.reload();
            }    
            })
          }
          catch(error)
          {
            alert("Incorrect email or password");
          }
        };

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
                    <input value={passwordValue} onChange={handlePasswordChange} className="text-box" type={inputType} placeholder="Password..."></input>
                    <img onClick={handleClick} src={eye} className="eye-image"></img><br></br>
                    <Link className="auth-link" to='/resetPassword'>Forgot Password?</Link><br></br><br></br>
                    <Link className="auth-link" to='/signUp'>Don't have an account?</Link>
                    <GoogleSignIn/>
                    <div onClick={handleSubmit} className="auth-button">Sign In</div>
                </div>
            </div>
        </>
    )
}

export default SignIn;