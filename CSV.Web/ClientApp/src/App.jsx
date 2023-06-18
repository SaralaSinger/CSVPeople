import React from 'react';
import { Route, Routes } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Home from './Home';
import Upload from './Upload';
import Generate from './Generate';
import Layout from './Layout'

const App = () => {
    return (
            <Layout>
                <Routes>
                    <Route exact path='/' element={<Home />} />
                    <Route exact path='/upload' element={<Upload />} />
                    <Route exact path='/generate' element={<Generate />} />
                </Routes>
            </Layout>
    );

}

export default App;