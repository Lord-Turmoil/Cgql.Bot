import './LoadCircle.css'

import loadingImage from '~/assets/img/loading.svg'

export function LoadCircle({ sx = {} }) {
    return (
        <div className='LoadCircle' style={{ ...sx }}>
            <img src={loadingImage} alt="loading" />
        </div >
    );
}