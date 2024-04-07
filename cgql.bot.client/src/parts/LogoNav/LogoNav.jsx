import { useNavigate } from 'react-router-dom';

import './LogoNav.css'
import Logo from '../Logo/Logo';

/*
data: {
    repository: string,
    repositoryUrl: string,

    branch: string,

    commit: string,
    commitUrl: string
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
                    <span>Repository: <a href={data.repositoryUrl} target='_blank'>{data.repository}</a></span>
                    <i></i>
                    <span>Branch: <a href={data.branchUrl} target='_blank'>{data.branch}</a></span>
                    <i></i>
                    <span>Commit: <a href={data.commitUrl} target='_blank'>{data.commit}</a></span>
                </div>
            }
        </div>
    );
}