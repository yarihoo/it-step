import { useState } from 'react';
import '../css/auth.css'
import { Link, useNavigate, useSearchParams } from 'react-router-dom';
import axios from 'axios';

const reset = async (data: IConfirm) => {
    const response = await axios.post("https://localhost:7048/confirmAccount", data);
    alert(`Account Confirmed`);
};

interface IConfirm {
    userId: string|null,
    token: string|null,
  }


const ConfrirmAccount  = () =>{
    let [searchParams] = useSearchParams();

    const [state, setState] = useState<IConfirm>({
        userId: searchParams.get("userId"),
        token: searchParams.get("code"),
      });
      console.log("Model : ", state);

    const handleConfirm = () => {
      try
        {        
            reset(state);
        }
        catch(error)
        {
          alert(error);
        }
      };

      handleConfirm();

    return(
        <>
            <div className="auth-container">
                <div className="auth-frame">
                    <div className="mini-logo">
                        <span style={{color: "white"}}>Burger</span>
                        <span style={{color: "yellow", textDecoration: "underline"}}> Bensonhurst</span>
                        <span style={{color: "yellow", fontSize: "2.5vw"}}> EST.1913</span>
                    </div>
                    <h1 style={{color: "white"}}>Account Confirmed</h1>
                   <br></br>
                    <Link to='/' className="auth-button">Home</Link>
                </div>
            </div>
        </>
    )
}

export default ConfrirmAccount;