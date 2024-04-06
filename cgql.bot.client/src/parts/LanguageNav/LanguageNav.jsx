import './LanguageNav.css'

import c_logo from '~/assets/img/c.svg';
import cpp_logo from '~/assets/img/cpp.svg';
import java_logo from '~/assets/img/java.svg';
import python_logo from '~/assets/img/python.svg';

const LANG_MAP = {
    'c': [c_logo, 'C'],
    'cpp': [cpp_logo, 'C++'],
    'java': [java_logo, 'Java'],
    'python': [python_logo, 'Python']
}

export default function LanguageNav({ language, id }) {
    return (
        <div className='LanguageNav'>
            <div className='LanguageNav__language'>
                {renderLanguage(language)}
            </div>
            <div className='LanguageNav__right'>
                {renderScanTaskId(id)}
            </div>
        </div>
    );

    function renderLanguage(lang) {
        const [logo, name] = LANG_MAP[lang];
        return (
            <span>Language: <img alt={name} src={logo} /> <b>{name}</b></span>
        );
    }

    function renderScanTaskId(id) {
        return (
            <span>Scan Task ID: <b>{id}</b></span>
        );
    }
}