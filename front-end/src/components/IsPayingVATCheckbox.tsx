interface Props{
    isPayingVAT: boolean
    handleIsPayingVATChange: ()=>void
}
export default function IsPayingVATCheckbox(props : Props){
    return(
        <>
        <label>Are you paying VAT?</label>  
        <input type="checkbox" onChange={props.handleIsPayingVATChange} checked={props.isPayingVAT}/>
        </>
    );
}