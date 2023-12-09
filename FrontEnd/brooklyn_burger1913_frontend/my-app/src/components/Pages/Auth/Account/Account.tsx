import { useState, useEffect } from 'react';
import "../css/auth.css"
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import eye_img from '../../Images/eye_icon.png'
import { IUserItem } from '../../../Interfaces';
import { reset } from '../../../Functions';
import default_img from "../../../Images/default_account_logo_png.png"


const Account = () =>{
  const handlePasswordChange = () => {
    try
      {
          reset(emailValue);
      }
      catch(error)
      {
        alert(error);
      }
    };
    
    async function GetUserImage()
    {
        const request = { data: localStorage.getItem('BurgerJwtToken') };
        await axios.post("https://localhost:7048/GetAccountImage", request).then((resp) =>{
          if (`${resp.data}`.length !== 0)
          {  setImageUrl('data:image/png;base64, ' + resp.data)}
        });
    }

    const handleFileSelect = (event: React.ChangeEvent<HTMLInputElement>) => {
      const file = event.target.files?.[0];
      if (file) {
        setSelectedFile(file);
        console.log(file);
        const imageUrl = URL.createObjectURL(file);
        const imgBox = document.getElementById('img-box') as HTMLImageElement;
        if (imgBox) {
          imgBox.src = imageUrl;
          localStorage.setItem('imageOnSet', imageUrl);
          navigate(`/cropper`);
        }
      }
    };
    const [imageUrl, setImageUrl] = useState<string>(default_img);
    const [selectedFile, setSelectedFile] = useState<File | null>(null);
    const [emailValue, setEmailValue] = useState('');
    const [firstNameValue, setFirstNameValue] = useState('');
    const [lastNameValue, setLastNameValue] = useState('');
    const [user, setUser] = useState<IUserItem>();
    
    async function GetUser()
    {
        const request = { data: localStorage.getItem('BurgerJwtToken') };
        await axios.post("https://localhost:7048/GetUser", request).then((resp) =>{
        setUser(resp.data);
        setEmailValue(resp.data.email);
        setFirstNameValue(resp.data.firstName);
        setLastNameValue(resp.data.lastName);
        });
    }
    useEffect(() => {
      GetUser();
      GetUserImage();
    }, []);

  function handleEmailChange(event: React.ChangeEvent<HTMLInputElement>) {
    setEmailValue(event.target.value);
  }

  function handleFirstNameChange(event: React.ChangeEvent<HTMLInputElement>) {
    setFirstNameValue(event.target.value);
  }

  function handleLastNameChange(event: React.ChangeEvent<HTMLInputElement>) {
    setLastNameValue(event.target.value);
  }
  const navigate = useNavigate();
    async function handleSubmit() {
      const u: IUserItem = 
      { 
          id: 0, 
          email: emailValue,
          firstName: firstNameValue,
          lastName: lastNameValue
      };
      console.log(u);
      const request = { jwt: localStorage.getItem('BurgerJwtToken') };

      await axios.post("https://localhost:7048/UpdateUser",
       {
          jwt: localStorage.getItem('BurgerJwtToken'),
          user: u
      }).then((resp)=>{
          const token = resp.data.token;
          localStorage.setItem('BurgerJwtToken', token.result);
          navigate("/");
      });
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
                    <label className='account-image-picker' htmlFor="img-picker">
                      <img id='img-box' className='acc-img' src={imageUrl} alt={default_img}></img>
                    </label>
                    <input onChange={handleFileSelect} id='img-picker' type='file'></input>
                    <input value={emailValue} onChange={handleEmailChange} className="text-box" type="email" placeholder="Email..."></input>
                    <input value={firstNameValue} onChange={handleFirstNameChange} className="text-box" type="email" placeholder="First Name..."></input>
                    <input value={lastNameValue} onChange={handleLastNameChange} className="text-box" type="email" placeholder="Last Name..."></input>
                    <br></br>
                    <div onClick={handlePasswordChange} className="auth-link">Change Password</div>
                    <div onClick={handleSubmit} className="auth-button">Submit</div>
                </div>
            </div>
        </>
    )
}

export default Account;