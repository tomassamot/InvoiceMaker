interface Props{
    confirmPassword : string
    handleConfirmPasswordChange : (newUsername: React.ChangeEvent<HTMLInputElement>)=>void
}
export default function ConfirmPasswordField(props : Props){
    return(
        <>
        <label>Confirm password:</label>  
        <input type={"password"} onChange={props.handleConfirmPasswordChange} value={props.confirmPassword}/>
        </>
    );
}