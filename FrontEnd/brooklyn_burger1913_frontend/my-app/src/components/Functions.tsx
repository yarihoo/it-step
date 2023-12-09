import React from 'react';
import axios from "axios";
import { Link, useAsyncValue, useNavigate  } from 'react-router-dom';



export async function AddToBag(Id: number, count: number){
        try
        {
            await axios.post("https://localhost:7048/AddToBag", {
            productId: Id,
            count: count,
            jwt: localStorage.getItem('BurgerJwtToken')
            });
            alert("Added to bag");
        }
        catch(error){
            alert(error);
        };

}

export function LogOut(){
    if(localStorage.getItem("BurgerJwtToken") !== null)
    {
        localStorage.removeItem("BurgerJwtToken");
    }
}

export function logOut(): Promise<void> {
  return new Promise<void>((resolve) => {
    localStorage.removeItem("BurgerJwtToken");
    localStorage.removeItem('Account-image');
    resolve();
  }).then(() => {
    window.location.reload();
  });
}

export const reset = async (Email: string) => {
    const data = { email: Email};
    try {
      const response = await axios.post("https://localhost:7048/forgotPassword", data);
      alert(`Mail sent to ${Email}`);
    } catch (error) {
      alert(error);
    }
  };

  export const reloadPage = (delay: number): void => {
    setTimeout(() => {
      window.location.reload();
    }, delay);
  };
