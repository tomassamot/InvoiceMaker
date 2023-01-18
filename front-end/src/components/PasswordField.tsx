interface Props{
    password : string
    handlePasswordChange: (newUsername: React.ChangeEvent<HTMLInputElement>)=>void
}
export default function PasswordField(props : Props){
    return(
        <>
        <label>Password:</label>  
        <input type={"password"} onChange={props.handlePasswordChange} value={props.password}/>
        </>
    );
}