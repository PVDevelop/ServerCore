import React from "react";
import { browserHistory } from "react-router";
import Form from 'react-bootstrap/lib/Form';
import Button from 'react-bootstrap/lib/Button';
import Modal from 'react-bootstrap/lib/Modal';

export default class RegistrationFailureModal extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            showModal: true
        };
        this.onOkButtonClicked = this.onOkButtonClicked.bind(this);
    }

    onOkButtonClicked(e) {
        this.setState({ showModal: false });
    }

    render() {
        return (
            <Modal show={this.state.showModal}>
                <Modal.Header>
                    <Modal.Title>Пользователь не был зарегистрирован.</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <h4></h4>
                    <p>При регистрации пользователя произошла ошибка. Убедитесь в корректности введенных данных и повторите попытку снова.</p>
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