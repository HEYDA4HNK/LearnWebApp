import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom'
import './index.css'
import App from './App.jsx'
import Login from './Login.jsx'
import Profile from './Profile.jsx'
import Register from './Register.jsx'
import { AuthProvider } from './context/AuthContext.jsx'

createRoot(document.getElementById('root')).render(
    <StrictMode>
        <AuthProvider>
            <BrowserRouter>
                <nav>
                    <Link to="/">Главная</Link> |
                    <Link to="/profile">Профиль</Link> |
                    <Link to="/register">Регистрация</Link> |
                    <Link to="/login">Войти</Link>
                </nav>

                <div>
                    <Routes>
                        <Route path='/' element={<App />} />
                        <Route path='/profile' element={<Profile />} />
                        <Route path='/register' element={<Register />} />
                        <Route path='/login' element={<Login />} />
                    </Routes>
                </div>

            </BrowserRouter>
        </AuthProvider>
        
    </StrictMode>
)
