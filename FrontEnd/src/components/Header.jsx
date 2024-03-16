import { Link } from 'react-router-dom'
import './Header.css'
import menu from '../assets/menu-icon.svg'



function Header() {
    return (
        <header>
            <div className='nav'>
                <Link to="/gestao" className='liniker'>Produto</Link>
                <a href="https://almoxarifado-tau.vercel.app/" target='_blank' className='liniker'>Requisição</a>
                <Link to="/" className='liniker'>BenchMarking Log</Link>
            </div>
            <img src={menu} className='icon' alt="" />
        </header>
    )
}

export default Header;