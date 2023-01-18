interface Props{
    username : string;
    handleUsernameChange: (newUsername: React.ChangeEvent<HTMLInputElement>)=>void;
}
export default function UsernameField(props : Props){
    return(
        <>
        <label>Username:</label>
        <input type={"text"} onChange={props.handleUsernameChange} value={props.username}/>
        </>
    );
}