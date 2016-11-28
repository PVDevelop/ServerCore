import React from "react";
import Modal from "react-bootstrap/lib/Modal";
import Button from "react-bootstrap/lib/Button";

export default class RegistrationSuccessModal extends React.Component {
    render() {
        return (
            <Modal
                show={this.props.show}
                dialogClassName="custom-modal">
                <Modal.Header>
                    <Modal.Title>Создание нового пользователя</Modal.Title>
                </Modal.Header>

                <Modal.Body>
                    Пользователь успешно создан, письмо с подтверждением регистрации
                    отправлено на указанный почтовый адрес. Для продолжения, перейдите по
                    ссылке, указанной в письме.
                    </Modal.Body>

                <Modal.Footer>
                    <Button
                        bsStyle="primary"
                        onClick={this.props.onOkClicked}>OK</Button>
                </Modal.Footer>

            </Modal>
        );
    }
}