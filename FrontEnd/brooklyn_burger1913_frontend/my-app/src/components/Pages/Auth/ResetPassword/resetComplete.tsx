import { useState } from 'react';
import '../css/auth.css'
import { Link, useNavigate, useSearchParams } from 'react-router-dom';
import axios from 'axios';

const reset = async (data: ChangePasswrodForm) => {
  const response = await axios.post("https://localhost:7048/changePassword", data);
  alert(`Password updated successfully`);
};


interface ChangePasswrodForm {
  userId: string|null,
  token: string|null,
  password: string;
  confirmPassword: string;
}

const ResetComplete  = () =>{
    const [passwordValue, setPasswordValue] = useState('');
    const [confirmPasswordValue, setConfirmPasswordValue] = useState('');
    function handlePasswordChange(event: React.ChangeEvent<HTMLInputElement>) {
      setPasswordValue(event.target.value);
    }
    function handleConfirmPasswordChange(event: React.ChangeEvent<HTMLInputElement>) {
      setConfirmPasswordValue(event.target.value);
    }
    
    let [searchParams] = useSearchParams();

    const [state, setState] = useState<ChangePasswrodForm>({
        userId: searchParams.get("userId"),
        token: searchParams.get("code"),
        password: "",
        confirmPassword: ""
      });
      console.log("Model : ", state);

    const navigate = useNavigate()

    const handleSubmit = () => {
      try
        {
            let value: ChangePasswrodForm = {
              userId: state.userId,
              token: state.token,
              password: passwordValue,
              confirmPassword: confirmPasswordValue
            }
            reset(value);
            if (localStorage.getItem("BurgerJwtToken") === null)
              navigate("/signIn");
            else
              navigate("/account");
        }
        catch(error)
        {
          alert(error);
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
                    <div className='password-container'>
                        <input value={passwordValue} onChange={handlePasswordChange} className="text-box password-box" type="password" placeholder="Password..."></input>
                    </div>
                    <input value={confirmPasswordValue} onChange={handleConfirmPasswordChange} className="text-box" type="password" placeholder="Confirm Password..."></input><br></br>
                   <br></br>
                    <Link onClick={handleSubmit} to='/signIn' className="auth-button">Change Password</Link>
                </div>
            </div>
        </>
    )
}

export default ResetComplete;