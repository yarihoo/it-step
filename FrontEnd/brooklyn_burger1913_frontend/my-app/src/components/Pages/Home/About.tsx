import React from 'react';
import background_image from '../../Images/manhattan-skyline.jpg'

const About = () => {
    return (
        <div className='product-table'>
            <div className='about-image-frame'>
                <img className='about-image' src={background_image}></img>
            </div>
            <div className='about-text'>
                Hello, User, my name is Yaroslav Halushko, currently located in Bensonhurst, Brooklyn,
                New York.
                I'm full-stack web-developer, my main specialization is creating frontend with React Typescript,
                and backend with C# ASP.NET Web API, the website you're seeing right now is one of projects,
                you can create account on "Burger Bensonhurst" clicking the "Account" on the header of website,
                and confirm the email getting special link on email letter, or log in if you're already signed up,
                you can add items you like to bag, you can see new category of burgers "Black Burgers" and also
                get something with special price.<br></br><br></br>
                I'm glad you visited my "Burger Bensonhurst" website, User!<br></br><br></br>
                Find me:<br></br>
                Email: <a className='selectable'>
                    yaroslav.halushko.n@gmail.com
                </a><br></br>
                LinkedIn: <a className='selectable' style={{color: 'yellow'}} href='https://www.linkedin.com/in/yaroslav-halushko'>
                    https://www.linkedin.com/in/yaroslav-halushko
                </a><br></br>
                GitHub Repositories: <a className='selectable' style={{color: 'yellow'}} href='https://github.com/Yaroslavhal?tab=repositories'>
                    https://github.com/Yaroslavhal?tab=repositories
                </a><br></br>
            </div>
        </div>
    )
}

export default About;