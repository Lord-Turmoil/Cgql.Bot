import { useNavigate } from 'react-router-dom';

import './LogoNav.css'
import Logo from '../Logo/Logo';

/*
data: {
    username: string,
    repository: string
}
*/
export default function LogoNav({ data = null, online = false }) {
    const navigate = useNavigate();
    const handleClick = () => {
        navigate('/');
    }

    return (
        <div className='NavBar'>
            <div className='NavBar__logo' onClick={handleClick}>
                <Logo online={online} fast={true} />
                <h3>CodeGraphQL.Bot</h3>
            </div>
            {data == null ? null :
                <div className='NavBar__right'>
                    <span>Username: Lord Turmoil</span>
                    <i>|</i>
                    <span>Repository: Turmoil/Subject</span>
                </div>
            }
        </div>
    );
}