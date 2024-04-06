import { useEffect, useState } from 'react';

// Anything between opening and closing tags goes into children prop.
export default function InflateBox({ children, overflow = false, minimum = 0, sx = {} }) {
    const [height, setHeight] = useState(overflow ? 'auto' : '100%');
    const [minHeight, setMinHeight] = useState('100%');

    function onResize() {
        const newHeight = Math.max(window.innerHeight, minimum) + 'px';
        setMinHeight(newHeight);
        setHeight(overflow ? 'auto' : newHeight);
    }

    useEffect(() => {
        onResize();
        window.removeEventListener('resize', onResize);
        window.addEventListener('resize', onResize);
    }, []);

    return (
        <div className="Inflate_Box clearfix" style={{ ...sx, position: 'relative', height: height, minHeight: minHeight }}>
            {children}
        </div>
    );
}