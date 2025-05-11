package com.hotel.group.hotelserver;

import java.util.Arrays;
import java.util.Iterator;
import java.util.List;
import java.util.NoSuchElementException;
import java.util.Set;
import javax.xml.namespace.QName;
import javax.xml.soap.*;
import javax.xml.ws.handler.MessageContext;
import javax.xml.ws.handler.soap.SOAPHandler;
import javax.xml.ws.handler.soap.SOAPMessageContext;
import javax.xml.ws.soap.SOAPFaultException;

public class Handler implements SOAPHandler<SOAPMessageContext> {
    
    List<String> testedMethodNames = Arrays.asList(
            "getReservations",
            "makeReservation",
            "modifyReservation",
            "cancelReservation",
            "requestReservationConfirmation");

    @Override
    public Set<QName> getHeaders() {
        return null;
    }

    @Override
    public boolean handleMessage(SOAPMessageContext context) {
        
        Boolean isRequest = (Boolean) context.get(MessageContext.MESSAGE_OUTBOUND_PROPERTY);
        
        if (!isRequest && testedMethodNames.contains(((QName) context.get(MessageContext.WSDL_OPERATION)).getLocalPart())) {
            try {
                SOAPMessage soapMessage = context.getMessage();
                SOAPEnvelope soapEnvelope = soapMessage.getSOAPPart().getEnvelope();
                SOAPHeader soapHeader = soapEnvelope.getHeader();
                
                
                if (soapHeader == null) {
                    soapHeader = soapEnvelope.addHeader();
                    
                    generateSOAPErrMessage(soapMessage, "No SOAP header");
                }
                
                Iterator iterator = soapHeader.extractAllHeaderElements();
                
                if (iterator == null || !iterator.hasNext()) {
                    generateSOAPErrMessage(soapMessage, "No header block for next actor");
                }
                
                Node node = (Node) iterator.next();
                String username = node == null ? null : node.getValue();
                
                if (!iterator.hasNext()) {
                    generateSOAPErrMessage(soapMessage, "No header block for next actor");
                }
                
                node = (Node) iterator.next();
                String password = node == null ? null : node.getValue();
                
                SOAPBody soapBody = soapMessage.getSOAPBody();
                int userId = Integer.parseInt(soapBody.getFirstChild().getLastChild().getTextContent());
                
                try {
                    User user = Users.findUser(username);
                    if (!user.getPassword().equals(password))
                        generateSOAPErrMessage(soapMessage, "Unauthorized");
                    if (user.getId() != userId)
                        generateSOAPErrMessage(soapMessage, "Unauthorized1");
                } catch (NoSuchElementException e) {
                    generateSOAPErrMessage(soapMessage, "Access denied");
                }
            } catch (SOAPException e) {
                System.err.print(e);
            }
        }
        return true;
    }
    
    private void generateSOAPErrMessage(SOAPMessage soapMessage, String reason) {
        try {
            SOAPBody soapBody = soapMessage.getSOAPPart().getEnvelope().getBody();
            SOAPFault soapFault = soapBody.addFault();
            soapFault.setFaultString(reason);
            throw new SOAPFaultException(soapFault);
        } catch (SOAPException ignored) { }
    }

    @Override
    public boolean handleFault(SOAPMessageContext context) {
        return true;
    }

    @Override
    public void close(MessageContext context) {

    }
    
}
