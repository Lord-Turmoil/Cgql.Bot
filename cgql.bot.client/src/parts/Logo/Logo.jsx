import './Logo.css'

export default function Logo({ online = false, fast = false }) {

    const cls = 'Logo' + (online ? ' Logo__online' : ' Logo__offline') + (fast ? ' Logo__fast' : ' Logo__slow');

    return (
        <div className={cls}>
            <img className='Logo__image' alt='favicon' src='/favicon.png'></img>
        </div>
    );
}