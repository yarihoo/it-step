import React, {useState, useEffect} from 'react';
import { Link } from 'react-router-dom';
import "./css/home.css"
import wall from "../../Images/brick_wall.jpg"
import { relative } from 'path';
import { idText } from 'typescript';
import { IProductItem, ICategoryItem, ISubcategoryItem, IBagItem } from '../../Interfaces';
import interior_img from "../../Images/restaurant_interior.jpg";
import axios from 'axios';

const Bag = () =>{
    const [items, setItems] = useState<Array<IBagItem>>([]);
    const [sum, setSum] = useState<number>(20.99);

    
    async function DeleteFromBag(id: number){
        if (localStorage.getItem('BurgerJwtToken') !== null)
        {
            try{
                await axios.post("https://localhost:7048/DeleteBagItem", {
                    id: id,
                    jwt: localStorage.getItem('BurgerJwtToken')
                });
                axios.post<Array<IBagItem>>('https://localhost:7048/GetBagItems', {
                jwt: localStorage.getItem("BurgerJwtToken")
                }).then(resp => {
                    setItems(resp.data);
                });
                axios.post("https://localhost:7048/GetFullPrice", { data: localStorage.getItem("BurgerJwtToken")}).then((resp)=>{
                setSum(resp.data);
                });
            }
            catch (error) {
                alert(error);
            }
        }
    }
    async function ChangeItemCount(elementId: string){
        let item = document.getElementById(elementId) as HTMLInputElement;
        let Id = Number(elementId.substring(3));
        let count = Number(item.value);
        try
        {
            await axios.post("https://localhost:7048/ChangeBagItemCount", {
            id: Id,
            count: count,
            jwt: localStorage.getItem('BurgerJwtToken')
            });
            axios.post("https://localhost:7048/GetFullPrice", { data: localStorage.getItem("BurgerJwtToken")}).then((resp)=>{
            setSum(resp.data);
            });
        }
        catch(error){
            alert(error);
        };
    }

    useEffect(() => {

        axios.post<Array<IBagItem>>('https://localhost:7048/GetBagItems', {
          jwt: localStorage.getItem("BurgerJwtToken")
        }).then(resp => {
            setItems(resp.data);
        });
        axios.post("https://localhost:7048/GetFullPrice", { data: localStorage.getItem("BurgerJwtToken")}).then((resp)=>{
          setSum(resp.data);
        });
      
      }, []);

    function GetPrice(product: IProductItem){
        if (product.salePrice !== 0){
            return(
                <>
                    <span style={{textDecoration: "line-through"}}>{ product.price }$</span><br></br>
                    <span style={{color: "yellow"}}>{ product.salePrice }$</span><br></br>
                </>
            )
        }
        else{
            return(
               <span>{ product.price }$</span>
            )
        }
    }
    if (items.length > 0){
        return (
            <>
            <div style={{
                    backgroundColor: "black",
                    width: "100%", 
                    position: "absolute",
                    minHeight: "80vw",
                }}>
                <table style={{marginTop: "100px"}}>
                <thead>
                    <tr>
                    <th></th>
                    <th></th>
                    <th></th>
                    <th></th>
                    <th></th>
                    <th></th>
                    </tr>
                </thead>
                <tbody>
                {items.map(item => (
                    <tr key={`${item.id}`}>
                        <td className='product-text'><img className='product-image' src={item.product.image}/></td>
                        <td className='product-text'>{ item.product.name }</td>
                        <td className='product-text'>{ item.product.description }</td>
                        <td className='product-text'>
                            {GetPrice(item.product)}
                        </td>
                        <td className='product-text'><input id={`cnt${item.id}`} onClick={()=>{ChangeItemCount(`cnt${item.id}`)}} className='counter' type='number' min={1} max={50} defaultValue={item.count}></input></td>
                        <td className='product-text'><div onClick={() => {DeleteFromBag(item.id)}} className='add-to-bag'>Delete</div></td>
                        
                    </tr>
                ))}
                </tbody>
                </table>
                <div className='checkout-btn'>Pay <span style={{fontSize: "4vw"}}>{sum}$</span></div>
                </div>
            </>
        )  
    }
    else{
        return(
            <>
                <div className='empty-box'>Bag Is Empty</div>
                <Link to="/menu" style={{display: 'block'}} className='checkout-btn'>Shop</Link>
            </>

        )
    }
}

export default Bag;