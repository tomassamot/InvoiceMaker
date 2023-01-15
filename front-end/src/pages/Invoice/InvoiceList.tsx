import { useNavigate } from "react-router-dom";

export default function InvoiceList() {
  const navigate = useNavigate();
    return (
  <>
  i am invoice list
      <button onClick={()=>navigate("/invoices/1")}>
        click me for invoice
      </button>
    </>
    );
  }