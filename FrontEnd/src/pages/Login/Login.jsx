import './Login.css'
import { Link } from 'react-router-dom'

function Login() {
  

  return (
    <>
    <div className='container-login'>
      <div className='box'>
      <div className='boxImagem'>
            <img src=".\src\Images\ImagemLogin.png" alt="" className='imagemLogin' />
        </div>
        <div className='boxLogin'>
            <div className='caixaLogin'>
              <h1 className='title'>Login</h1>
                <div>
                <img src="./src/assets/user.svg" alt="" /> 
                <hr></hr>
                <input placeholder="Usuário" className='login-input' type="text" />
                </div>
                
                <div>
                <img src="./src/assets/password.svg" alt="" /> 
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