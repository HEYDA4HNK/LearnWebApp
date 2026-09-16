import { useContext } from 'react'
import { AuthContext } from './context/AuthContext.jsx'
import axios from 'axios'

function Register() {
    const {
        token,
        setToken
    } = useContext(AuthContext);

    async function regFunc(userName, email, password) {
        const response = await axios.post('http://localhost:5160/api/users/register',
            {
                Name: userName,
                Email: email,
                Password: password
            },
            {
                headers: {
                    'Content-Type': 'application/json'
                },
            });
        let data = response.data;
        if (data) {
            
            let jwt = data.token;
            setToken(jwt);
            console.log(`Register OK token is - ${jwt}`)
        }
        else {
            console.error(`Error: ${data.error}`)
        }
    }

    const reg = (form) => {
        
        //e.preventDefault()
        //const t = e.target;
        //const form = new FormData(t);
        const userName = form.get('userName');
        const email = form.get('email');
        const password = form.get('password');

        console.log(`REGISTER WITH - n:${userName}, email:${email}, password:${password}`)
        regFunc(userName, email, password);
    }

  return (
      <form action={reg}>
          <label htmlFor='userName'>Никнейм: </label>
          <input id='userName' name="userName" type='text' />
          <label htmlFor='userEmail'>Email: </label>
          <input id='userEmail' name="email" type='email' /> 
          <label htmlFor='password'>Пароль: </label>
          <input id='password' name="password" type='password' /> 
          <button type='submit'>Зарегистрироваться</button>
    </ form>
  )
}

export default Register
