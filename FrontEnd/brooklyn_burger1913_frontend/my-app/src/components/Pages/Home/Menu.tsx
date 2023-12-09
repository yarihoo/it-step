import React, {useState, useEffect} from 'react';
import { Link, Navigate, useAsyncValue, useNavigate  } from 'react-router-dom';
import "./css/home.css"
import wall from "../../Images/brick_wall.jpg"
import { relative } from 'path';
import { idText } from 'typescript';
import { IProductItem, ICategoryItem, ISubcategoryItem } from '../../Interfaces';
import interior_img from "../../Images/restaurant_interior.jpg"
import { AddToBag } from '../../Functions';
import axios from "axios";


//MAX SYMBOL COUNT FOR DEAL DESCRIPTION MUST BE 225 SYMBOLS
//MAX SYMBOL COUNT FOR DEAL NAME MUST BE 90 SYMBOLS

const Menu = () => {
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

   

    const Banner = () =>{
        return(
            <>
            <div style={{backgroundColor: 'black', paddingBottom: "2vw"}}>
                <div className='about-us'>
                    <img src={interior_img} className='interior-image'></img>
                    <div style={{fontSize: "1.7vw"}} className='about-us-banner-text'>
                    <p>&nbsp;&nbsp;&nbsp;Welcome to our burger restaurant located in the heart of Brooklyn Bensonhurst! Our passion for creating the best burgers in town is what drives us every day.</p>

                    &nbsp;&nbsp;&nbsp;Since our humble beginnings in year of 1913, we've been committed to providing our customers with the highest quality ingredients and exceptional service. From our juicy beef patties to our fresh toppings, we take pride in every burger we serve.
                    </div>
                </div>
                <div style={{width: "90%", margin: "0 5% 0 5%"}} className='about-us-banner-text'>
                    But what really sets us apart is our dedication to cooking with love. We believe that every burger should be made with care and attention to detail, and that's exactly what we do. Each burger is handcrafted with precision and cooked to perfection, ensuring that every bite is bursting with flavor.

                    Whether you're in the mood for a classic cheeseburger or something a little more adventurous, we've got you covered. Our menu features a wide variety of options, from our signature bacon and blue cheese burger to our vegetarian-friendly portobello burger.

                    So come on in and join us for a delicious meal that's sure to satisfy your cravings. We can't wait to share our love for burgers with you!
                </div>
            </div>
            </>

        )
    }

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
            {Categories.map(category => (
              <React.Fragment key={"category-" + category.id}>
                <div className='category-title'>
                  {category.name}
                </div>
                {category.subcategories.map(subcategory => (
                  <React.Fragment key={"subcategory-" + subcategory.id}>
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
                  </React.Fragment>
                ))}
              </React.Fragment>
            ))}
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
  
  export default Menu;