interface Props{
    usernameValidator: string;
    username : string;
    handleUsernameChange: (newUsername: React.ChangeEvent<HTMLInputElement>)=>void;
}
export default function UsernameField(props : Props){
    return(
        <>
        {
        props.usernameValidator !== "" ?
            (
            <label style={{color: "red"}}>* {props.usernameValidator}</label>
            )
            :""
        }
        <label>Username:</label>
        <input type={"text"} onChange={props.handleUsernameChange} value={props.username}/>
        </>
    );
}