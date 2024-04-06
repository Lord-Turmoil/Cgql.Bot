import { Route, Routes } from 'react-router-dom'
import '~/assets/css/App.css';
import HomePage from '~/pages/HomePage/HomePage';

function App() {
    return (
        <Routes>
            <Route exact path="/" element={<HomePage></HomePage>}></Route>
            {/* <Route exact path="/results/:id" element={<HomePage></HomePage>}></Route> */}
        </Routes>
    );
}

export default App;