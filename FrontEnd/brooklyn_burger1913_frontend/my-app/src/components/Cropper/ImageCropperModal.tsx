import React, { useState, useRef } from "react";
import ReactCrop, { Crop, ReactCropProps } from "react-image-crop";
import default_img from "../Images/default_account_logo_png.png"
import 'react-image-crop/dist/ReactCrop.css'
import axios from "axios";
import { Link, useNavigate, useParams } from "react-router-dom";

interface ImageCropperProps {
    imageUrl: string;
}


const ImageCropper = () => {
    async function handleSaving(imgUrl: string){
        try
        {
            await axios.post("https://localhost:7048/SaveUserImage",
            {
               jwt: localStorage.getItem('BurgerJwtToken'),
               imageUrl: imgUrl
    
            }).then((resp)=>{
                navigate('/account');
               console.log('image saved');
               localStorage.setItem('Account-image', 'data:image/png;base64, ' + imgUrl);
               window.location.reload(); 
            });
        }
        catch(error){
            alert(error);
        }
    }
    
    const imgUrl = localStorage.getItem('imageOnSet') + "";
    const [crop, setCrop] = useState<Crop>({
        unit: '%',
        x: 25,
        y: 25,
        width: 50,
        height: 50
      })
    const [path, setPath] = useState<string>(default_img);
    const imageRef = useRef<HTMLImageElement>(null);
    const navigate = useNavigate();
    const handleSave = () => {
        if (imageRef.current && crop.width && crop.height) {
            const canvas = document.createElement("canvas");
            const scaleX = imageRef.current.naturalWidth / imageRef.current.width;
            const scaleY = imageRef.current.naturalHeight / imageRef.current.height;
            canvas.width = crop.width;
            canvas.height = crop.height;
            const ctx = canvas.getContext("2d");
            if (ctx) {
                ctx.drawImage(
                    imageRef.current,
                    crop.x * scaleX,
                    crop.y * scaleY,
                    crop.width * scaleX,
                    crop.height * scaleY,
                    0,
                    0,
                    crop.width,
                    crop.height
                );
                const dataURL = canvas.toDataURL();
                const encodedImage = dataURL.split(",")[1];
                handleSaving(encodedImage);
            }
        }
    }

    return (
        <>
        <div style={{height: "130px"}}></div>
            <div className="cropper-container">
                <ReactCrop crop={crop} onChange={c => {setCrop(c)}}>
                    <img className="cropped-image" ref={imageRef} src={imgUrl}/>
                </ReactCrop>
                <div style={{width: "100%"}}>
                    <div className="cropper-btn crop-btn" onClick={handleSave}>Save</div>
                    <Link to='/account' className="cropper-btn close-btn">Close</Link>
                </div>             
            </div>
        </>


    )
}

export default ImageCropper;

