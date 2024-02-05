import './Button.css'

function Button({children, onClick}){
    return (
        <button className="Button" onClick={onClick}>
            {children}
            </button>
    )
}

export default Button;