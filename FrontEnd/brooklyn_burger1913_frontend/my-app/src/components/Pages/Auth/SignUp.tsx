import { useState } from 'react';
import "./css/auth.css"
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import eye from '../../Images/see-password-image.png'
import GoogleSignIn from './Google/GoogleSignIn';
const SignUp = () =>{
    const navigate = useNavigate();
    async function handleSubmit(){
        try
        {
            await axios.post("https://localhost:7048/register",
            {
               firstName: firstNameValue,
               lastName: lastNameValue,
               email: emailValue,
               password: passwordValue,
               confirmPassword: confirmPasswordValue
            }).then((resp)=>{
               alert("Registered successfully");
               navigate("/signIn");
            });
        }
        catch(error){
            alert(error);
        }
    }
    const [inputType, setInputType] = useState<string>('password');

    const [input2Type, setInput2Type] = useState<string>('password');
    const handleClick = () => {
      setInputType(inputType === 'password' ? 'text' : 'password');
    };
    const handle2Click = () => {
      setInput2Type(input2Type === 'password' ? 'text' : 'password');
    };
    const [emailValue, setEmailValue] = useState('');
    const [firstNameValue, setFirstNameValue] = useState('');
    const [lastNameValue, setLastNameValue] = useState('');
    const [passwordValue, setPasswordValue] = useState('');
    const [confirmPasswordValue, setConfirmPasswordValue] = useState('');

  function handleEmailChange(event: React.ChangeEvent<HTMLInputElement>) {
    setEmailValue(event.target.value);
  }

  function handleFirstNameChange(event: React.ChangeEvent<HTMLInputElement>) {
    setFirstNameValue(event.target.value);
  }

  function handleLastNameChange(event: React.ChangeEvent<HTMLInputElement>) {
    setLastNameValue(event.target.value);
  }

  function handlePasswordChange(event: React.ChangeEvent<HTMLInputElement>) {
    setPasswordValue(event.target.value);
  }

  function handleConfirmPasswordChange(event: React.ChangeEvent<HTMLInputElement>) {
    setConfirmPasswordValue(event.target.value);
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
                    <input value={firstNameValue} onChange={handleFirstNameChange} className="text-box" type="email" placeholder="First Name..."></input>
                    <input value={lastNameValue} onChange={handleLastNameChange} className="text-box" type="email" placeholder="Last Name..."></input>
                    <input value={passwordValue} onChange={handlePasswordChange} className="text-box" type={inputType} placeholder="Password..."></input>
                    <img onClick={handleClick} src={eye} className="eye-image"></img>
                    <input value={confirmPasswordValue} onChange={handleConfirmPasswordChange} className="text-box" type={input2Type} placeholder="Confirm Password..."></input>
                    <img onClick={handle2Click} src={eye} className="eye-image"></img>
                    <br></br>
                    <GoogleSignIn/>
                    <Link className="auth-link" to='/signIn'>Already have an account?</Link>
                    <div onClick={handleSubmit} className="auth-button">Sign Up</div>
                </div>
            </div>
        </>
    )
}

export default SignUp;