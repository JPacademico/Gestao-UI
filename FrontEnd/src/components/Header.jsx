import { Link } from 'react-router-dom'
import './Header.css'
import menu from '../assets/menu-icon.svg'



function Header() {
    return (
        <header>
            <div className='nav'>
                <Link to="/gestao" className='liniker'>Produto</Link>
                <Link to="/log" className='liniker'>Logs</Link>
                <Link to="/itens" className='liniker'>Itens BenchMarking</Link>
                <a href="https://almoxarifado-tau.vercel.app/" target='_blank' className='liniker'>Requisição</a>
            </div>
            <img src={menu} className='icon' alt="" />
        </header>
    )
}

export default Header;