import './Login.css'
import { Link } from 'react-router-dom'
import LoginI from "../../Images/ImagemLogin.png";
import user from "../../assets/user.svg";
import password from "../../assets/password.svg";

function Login() {
  

  return (
    <>
    <div className='container-login'>
      <div className='box'>
      <div className='boxImagem'>
            <img src={LoginI} alt="" className='imagemLogin' />
        </div>
        <div className='boxLogin'>
            <div className='caixaLogin'>
              <h1 className='title'>Login</h1>
                <div>
                <img src={user}alt="" /> 
                <hr></hr>
                <input placeholder="Usuário" className='login-input' type="text" />
                </div>
                
                <div>
                <img src={password} alt="" /> 
                <hr></hr>
                <input placeholder="Senha" type="password" className='login-input'/>
                </div>
            </div>

            <div className='botoes'>
            <Link to={'/gestao'} className='btn'>Entrar</Link>
            <button className='btnCadastrar'>Novo? Cadastre-se</button>
            </div>
        </div>
      </div>
        
    </div>
      
    </>
  )
}

export default Login