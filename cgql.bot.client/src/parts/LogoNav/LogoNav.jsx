import { useNavigate } from 'react-router-dom';

import './LogoNav.css'

export default function LogoNav() {
    const navigate = useNavigate();
    const handleClick = () => {
        navigate('/');
    }

    return (
        <div className='NavBar'>
            <div className='NavBar__logo' onClick={handleClick}>
                <img src='/favicon.png' alt='logo' />
                <h3>CodeGraphQL.Bot</h3>
            </div>
            <div className='NavBar__right'>
                <span>Username: Lord Turmoil</span>
                <i>|</i>
                <span>Repository: Turmoil/Subject</span>
            </div>
        </div>
    );
}