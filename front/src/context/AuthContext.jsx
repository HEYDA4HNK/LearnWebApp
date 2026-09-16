import { createContext, useEffect, useState } from 'react'

export const AuthContext = createContext({});

export const AuthProvider = (props) =>
{
    const tokenStorageId = 'UserToken';

    const [token, setToken] = useState(localStorage.getItem(tokenStorageId));

    useEffect(() => {
        if (token) {
            localStorage.setItem(tokenStorageId, token);
        }
        else {
            localStorage.removeItem(tokenStorageId);
        }
            
        },
        [token]
    )

    const { children } = props;
    return(
        <AuthContext.Provider
            value={{
                token,
                setToken
            }}
        >
            { children }
        </AuthContext.Provider>
    )
}