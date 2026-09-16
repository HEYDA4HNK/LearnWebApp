import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import './index.css'
import { App, Login, Register, Profile } from './App.tsx'

createRoot(document.getElementById('root')!).render(
    < StrictMode >
        <div>
            <App />
        {/*
                <Routes>
                    <Route path='/' element={<App />} />
                    <Route path='/profile' element={<Profile />} />
                    <Route path='/register' element={<Register />} />
                    <Route path='/login' element={<Login />} />
                </Routes> */}
            </div>

        </StrictMode>
);
