import './Header.css'

function Header(){
    return(
        <header className="container-fluid py-4">
            <div className="d-flex">
                <h1>E-Car Shop</h1>
                <div className="d-flex w-100">
                    <ul>
                        <li>
                            <button>
                                <i className="bi bi-bag"></i>
                                Машины
                            </button>
                        </li>
                        <li>
                            <button>
                                <i className="bi bi-plus-circle"></i>
                                Добавить машину
                            </button>
                        </li>
                        <li>
                            <button>
                                <i className="bi bi-people"></i>
                                Пользователи
                            </button>
                        </li>
                    </ul>
                </div>
                <div className="d-flex justify-content-end">
                    <ul>
                        <li>
                            <button>
                                Войти
                            </button>
                        </li>
                    </ul>
                </div>
            </div>
        </header>
    )
}

export default Header;