import { Link } from 'react-router-dom'
import './Header.css'
import menu from '../assets/menu-icon.svg'



function Header() {
    return (
        <header>
            <div className='nav'>
                <Link to="/" className='liniker'>Produto</Link>
                <Link to="/" className='liniker'>Requisição</Link>
                <Link to="/" className='liniker'>BenchMarking Log</Link>
                <Link to="/" className='liniker'>Configurações</Link>
            </div>
            <img src={menu} className='icon' alt="" />
        </header>
    )
}

export default Header;