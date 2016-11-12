import React from "react";
import { browserHistory } from "react-router";
import Form from 'react-bootstrap/lib/Form';
import Button from 'react-bootstrap/lib/Button';
import Modal from 'react-bootstrap/lib/Modal';

export default class RegistrationSuccessModal extends React.Component {
    constructor(props) {
        super(props);
        this.onOkButtonClicked = this.onOkButtonClicked.bind(this);
    }

    onOkButtonClicked(e) {
        browserHistory.push('/');
    }

    render() {
        return (
            <Modal show={true}>
                <Modal.Header>
                    <Modal.Title>Пользователь успешно создан.</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <h4></h4>
                    <p>Подтверждение о регистрации отправлено на указанный почтовый адрес.</p>
                </Modal.Body>
                <Modal.Footer>
                    <Button
                        bsStyle="primary"
                        onClick={this.onOkButtonClicked}>
                        OK
                </Button>
                </Modal.Footer>
            </Modal>
        );
    }
}