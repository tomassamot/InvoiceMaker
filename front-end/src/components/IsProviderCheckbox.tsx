interface Props{
    isProvider : boolean
    handleIsProviderChange: ()=>void
}
export default function IsProviderCheckbox(props : Props){
    return(
        <>
        <label>Are you a provider?</label>
        <input type="checkbox" onChange={props.handleIsProviderChange} checked={props.isProvider}/>
        </>
    );
}