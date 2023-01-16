interface Props{
    passwordValidator : string
    password : string
    handlePasswordChange: (newUsername: React.ChangeEvent<HTMLInputElement>)=>void
}
export default function PasswordField(props : Props){
    return(
        <>
        {
        props.passwordValidator !== "" ?
            (
            <label style={{color: "red"}}>* {props.passwordValidator}</label>
            )
            :""
        }
        <label>Password:</label>  
        <input type={"password"} onChange={props.handlePasswordChange} value={props.password}/>
        </>
    );
}