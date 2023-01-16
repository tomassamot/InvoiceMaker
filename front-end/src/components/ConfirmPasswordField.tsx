interface Props{
    confirmPasswordValidator : string
    confirmPassword : string
    handleConfirmPasswordChange : (newUsername: React.ChangeEvent<HTMLInputElement>)=>void
}
export default function ConfirmPasswordField(props : Props){
    return(
        <>
        {
            props.confirmPasswordValidator !== "" ?
            (
            <label style={{color: "red"}}>* {props.confirmPasswordValidator}</label>
            )
            :""
        }
        <label>Confirm password:</label>  
        <input type={"password"} onChange={props.handleConfirmPasswordChange} value={props.confirmPassword}/>
        </>
    );
}