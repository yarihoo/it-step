import React, {useState, useEffect} from 'react';
import { Link, Navigate, useAsyncValue, useNavigate  } from 'react-router-dom';
import "./css/home.css"
import wall from "../../Images/brick_wall.jpg"
import { relative } from 'path';
import { idText } from 'typescript';
import { IProductItem, ICategoryItem, ISubcategoryItem } from '../../Interfaces';
import interior_img from "../../Images/restaurant_interior.jpg"
import { AddToBag } from '../../Functions';
import axios from 'axios';

//MAX SYMBOL COUNT FOR DEAL DESCRIPTION MUST BE 225 SYMBOLS
//MAX SYMBOL COUNT FOR DEAL NAME MUST BE 90 SYMBOLS

const BlackBurgers = () => {
    let navigate = useNavigate();
    function AddToBagNav(id: number){
        if (localStorage.getItem("BurgerJwtToken") !== null)
        {
            let count = (document.getElementById("count"+id) as HTMLInputElement).value;
            AddToBag(id, Number(count));
        }
        else{
            navigate('/signIn');
            alert("Sign in to add product to bag");
        }
    }
    
    const [categories, setCategories] = useState<Array<ICategoryItem>>([]);

    useEffect(() => {
        axios.get<Array<ICategoryItem>>('https://localhost:7048/Categories').then(resp => {
            setCategories(resp.data);
        });
    }, []);

    function GetPrice(product: IProductItem){
        if (product.salePrice !== 0){
            return(
                <>
                    <span style={{textDecoration: "line-through"}}>{ product.price }$</span><br></br>
                    <span style={{color: "yellow"}}>{ product.price }$</span><br></br>
                </>
            )
        }
        else{
            return(
               <span>{ product.price }$</span>
            )
        }
    }

    const PopularItems = (Categories: ICategoryItem[]) => {
        return (
          <div>
            {
              Categories.filter(x => x.name === "Burgers").map(category => (
                <div key={"category-" + category.id}>
                  <div className='category-title'>
                    {category.name}
                  </div>
                  {
                    category.subcategories.filter(x => x.name === "Black Burgers").map(subcategory => (
                      <div key={"subcategory-" + subcategory.id}>
                        <div className='subcategory-title'>{subcategory.name}</div>
                        <div style={{ width: "50%", margin: "1vw auto 3vw auto" }} className='white-line'></div>
                        <table className='product-table'>
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
                            {subcategory.products.map(product => (
                              <tr key={"product-" + product.id}>
                                <td className='product-text'><img className='product-image' src={product.image} /></td>
                                <td className='product-text'>{product.name}</td>
                                <td className='product-text'>{product.description}</td>
                                <td className='product-text'>
                                  {GetPrice(product)}
                                </td>
                                <td className='product-text'><input id={`count${product.id}`} className='counter' type='number' min={1} max={50} defaultValue={1}></input></td>
                                <td className='product-text'><div onClick={() => { AddToBagNav(product.id) }} className='add-to-bag'>Add To Bag</div></td>
                              </tr>
                            ))}
                          </tbody>
                        </table>
                      </div>
                    ))
                  }
                </div>
              ))
            }
          </div>
        );
      };

    return (
        <>
            <div style={{backgroundColor: "black", paddingTop: "100px"}}>{PopularItems(categories)}</div>
            <div style={{height: "600px"}}></div>
        </>
    );
  };
  
  export default BlackBurgers;